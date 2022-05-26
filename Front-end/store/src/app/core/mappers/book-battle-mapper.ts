import { Injectable } from '@angular/core';

import { BookBattleDto } from '../DTOs/book-battle-dto';
import { BookBattle } from '../models/book-battle';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class BookBattleMapper {
  /** @inheritdoc */
  public fromDto(data: BookBattleDto): BookBattle {
    return new BookBattle({
      firstBattleBook: data.firstBattleBook,
      secondBattleBook: data.secondBattleBook,
      endDate: new Date(data.endDate),
      state: data.state,
      discountPercentage: data.discountPercentage,
      finalDiscount: data.finalDiscount,
    });
  }
}
