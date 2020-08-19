export interface PagedCollection<T = any> {
  page: number;
  pageSize: number;
  pageCount: number;
  totalRecords: number;
  items: T[];
}

export interface Created {
  by: string;
  on: Date;
  process: string;
}

export interface Modified {
  by?: string;
  on?: Date;
  process?: string;
}
