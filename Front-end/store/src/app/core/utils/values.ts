export const PRODUCT_URL = 'https://comic-store-server.herokuapp.com/store/catalog';
export const SELECTION_URL = 'https://comic-store-server.herokuapp.com/store/selection/';
export const LOGIN_URL = 'https://comic-store-server.herokuapp.com/store/account/login';
export const REGISTER_URL = 'https://comic-store-server.herokuapp.com/store/account/registration';
export const REFRESH_URL = 'https://comic-store-server.herokuapp.com/store/account/refresh';
export const BASKET_URL = 'https://comic-store-server.herokuapp.com/store/basket/product';
export const PROFILE_URL = 'https://comic-store-server.herokuapp.com/store/profile';

export const SEARCH_URL = 'https://comic-store-server.herokuapp.com/store/selection/search';

export const ORDER_URL = 'https://comic-store-server.herokuapp.com/store/order';

export const BATTLE_URL = 'https://comic-store-server.herokuapp.com/store/battle';

export const TELEGRAM_URL = 'https://comic-store-server.herokuapp.com/store/telegram-bot';

export const TAGS_URL = 'https://comic-store-server.herokuapp.com/store/tag';

export const FILTERS_URL = 'https://comic-store-server.herokuapp.com/store/catalog/filter';

export const PAGE_SIZE = 5;
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
