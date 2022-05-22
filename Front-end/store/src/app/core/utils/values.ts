export const PRODUCT_URL = 'https://localhost:44327/store/catalog/';
export const SELECTION_URL = 'https://localhost:44327/store/selection/';
export const LOGIN_URL = 'https://localhost:44327/store/account/login';
export const REGISTER_URL = 'https://localhost:44327/store/account/registration';
export const REFRESH_URL = 'https://localhost:44327/store/account/refresh';
export const BASKET_URL = 'https://localhost:44327/store/basket/product';
export const PROFILE_URL = 'https://localhost:44327/store/profile';

export const ORDER_URL = 'https://localhost:44327/store/order';

export const BATTLE_URL = 'https://localhost:44327/store/battle';

export const TELEGRAM_URL = 'https://localhost:44327/store/telegram-bot';

export const PAGE_SIZE = 3;
export const PAGE_NUMBER = 1;

export const SEARCH_DEPTH = 3;
export const HINT_SIZE = 5;

export const SELECTION_SIZE = 5;

export const SEARCH_TARGET_GROUP = {
  Book: 'name',
  Author: 'authorName',
  Publisher: 'publisherName',
};

export const SEARCH_TARGETS: string[] = [SEARCH_TARGET_GROUP.Book, SEARCH_TARGET_GROUP.Author, SEARCH_TARGET_GROUP.Publisher];
