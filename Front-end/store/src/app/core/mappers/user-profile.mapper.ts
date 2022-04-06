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
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      phoneNumber: data.phoneNumber,
      address: data.address,
    };
  }

  /** @inheritdoc */
  public fromDto(dto: UserProfileDto): UserProfile {
    return new UserProfile({
      firstName: dto.firstName,
      lastName: dto.lastName,
      email: dto.email,
      phoneNumber: dto.phoneNumber,
      address: dto.address,
    });
  }
}
