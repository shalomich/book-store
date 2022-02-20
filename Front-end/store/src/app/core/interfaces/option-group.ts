import {SearchOptions} from "./search-options";
import {FilterOptions} from "./filter-options";
import {SortingOptions} from "./sorting-options";
import {PaginationOptions} from "./pagination-options";

export interface OptionGroup {
  pagingOptions: PaginationOptions,
  sortingOptions?: SortingOptions[],
  filterOptions?: FilterOptions
}
