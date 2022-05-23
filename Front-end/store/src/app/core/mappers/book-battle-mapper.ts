import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';

import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreview } from '../models/product-preview';

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
