export interface IToDtoMapper<TDto, TDomain> {

  toDto(data: TDomain): TDto;
}
