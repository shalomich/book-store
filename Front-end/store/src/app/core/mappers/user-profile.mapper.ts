import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';

import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreview } from '../models/product-preview';

import { UserProfileDto } from '../DTOs/user-profile-dto';
import { UserProfile } from '../models/user-profile';

import { Mapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class UserProfileMapper extends Mapper<UserProfileDto, UserProfile> {

  /** @inheritdoc */
  public toDto(data: UserProfile): UserProfileDto {
    return {
      id: data.id,
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      phoneNumber: data.phoneNumber,
      address: data.address,
      votingPointCount: data.votingPointCount,
      isTelegramBotLinked: data.isTelegramBotLinked,
      basketBookIds: data.basketBookIds,
      spentCurrentVotingPointCount: data.spentCurrentVotingPointCount,
      currentVotedBattleBookId: data.currentVotedBattleBookId,
      tagIds: data.tagIds,
    };
  }

  /** @inheritdoc */
  public fromDto(dto: UserProfileDto): UserProfile {
    return new UserProfile({
      id: dto.id,
      firstName: dto.firstName,
      lastName: dto.lastName,
      email: dto.email,
      phoneNumber: dto.phoneNumber,
      address: dto.address,
      votingPointCount: dto.votingPointCount,
      isTelegramBotLinked: dto.isTelegramBotLinked,
      basketBookIds: dto.basketBookIds,
      spentCurrentVotingPointCount: dto.spentCurrentVotingPointCount,
      currentVotedBattleBookId: dto.currentVotedBattleBookId,
      tagIds: dto.tagIds,
    } as UserProfile);
  }
}
