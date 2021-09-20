/**
 * Maps DTO into Model, or Model into DTO.
 */
export interface IMapper<TDto, TDomain> {

  /**
   * Maps from DTO to Domain model.
   */
  fromDto(data: TDto, relatedEntities: {}): TDomain;

  /**
   * Maps from Domain to DTO model.
   */
  toDto(data: TDomain): TDto;
}
