﻿using App.Attributes.GenericController;
using App.Areas.Store.Services;
using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;
using static App.Areas.Common.RequestHandlers.CreateHandler;
using static App.Areas.Common.RequestHandlers.UpdateHandler;
using static App.Areas.Common.RequestHandlers.DeleteHandler;
using static App.Areas.Common.RequestHandlers.GetPaggingMetadataHandler;
using App.Exceptions;
using App.Services.QueryBuilders;

namespace App.Areas.Dashboard.Controllers
{
    [ApiController]
    [Area("dashboard")]
    public abstract class FormEntityCrudController<TFormEntity, TForm> : Controller where TFormEntity : class, IFormEntity
        where TForm : IEntityForm
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get;}
        protected DbFormEntityQueryBuilder<TFormEntity> QueryBuilder { get; }

        protected FormEntityCrudController(IMediator mediator, IMapper mapper, DbFormEntityQueryBuilder<TFormEntity> queryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            QueryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        protected Type FormEntityType => typeof(TFormEntity);

        [HttpGet]
        public abstract Task<ActionResult<TForm[]>> Read([FromQuery] QueryTransformArgs args);


        [HttpGet("{id}")]
        public abstract Task<ActionResult<TForm>> Read(int id);


        [HttpHead]
        public async Task GetPaggingMetadata([FromQuery] QueryTransformArgs args)
        {
            QueryBuilder.AddDataTransformation(args);

            var metadata = await Mediator.Send(new GetMetadataQuery(args.Pagging, QueryBuilder));

            HttpContext.Response.Headers.Add(metadata);
        }

        private TFormEntity MapFromForm(TForm entityForm)
        {
            TFormEntity formEntity;
            try
            {
                formEntity = Mapper.Map<TFormEntity>(entityForm);
            }
            catch (Exception exception)
            {
                throw new BadRequestException(exception.InnerException.Message);
            }

            return formEntity;
        }

        [HttpPost] 
        public async Task<IActionResult> Create(TForm entityForm) 
        {
            var formEntity = MapFromForm(entityForm);

            var createdEntity = await Mediator.Send(new CreateCommand(formEntity));

            return CreatedAtAction(nameof(Read), new { id = createdEntity.Id }, Mapper.Map<TForm>(createdEntity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TForm entityForm)
        {
            var formEntity = MapFromForm(entityForm);

            await Mediator.Send(new UpdateCommand(id, formEntity));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedFormEntity = (IFormEntity) await Mediator.Send(new GetByIdQuery(id, QueryBuilder));
            await Mediator.Send(new DeleteCommand(deletedFormEntity));

            return NoContent();
        }
    }
}