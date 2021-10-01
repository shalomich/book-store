import { IFromDtoMapper } from './from-dto-mapper';
import { IToDtoMapper } from './to-dto-mapper';
import { Injectable } from '@angular/core';

export abstract class Mapper<TDto, TDomain> implements IFromDtoMapper<TDto, TDomain>,IToDtoMapper<TDto, TDomain> {
  abstract fromDto(data: TDto): TDomain

  abstract toDto(data: TDomain): TDto
}
