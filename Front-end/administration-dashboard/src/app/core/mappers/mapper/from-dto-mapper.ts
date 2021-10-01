export interface IFromDtoMapper<TDto, TDomain> {

  fromDto(data: TDto): TDomain;
}
