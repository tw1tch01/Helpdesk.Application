export interface PagedCollection<T = any> {
  page: number;
  pageSize: number;
  pageCount: number;
  totalRecords: number;
  items: T[];
}
