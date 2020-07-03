export class ShopParams {

  id = 0;
  productBrandId = 0;
  productTypeId = 0;
  sort = 'name';
  pageNumber = 1;
  pageSize = 6;
  search: string;


  resetParams() {
    return new ShopParams();
   }
}
