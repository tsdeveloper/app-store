export class ShopParams {

  productBrandId = 0;
  productTypeId = 0;
  sort = 'name';
  pageNumber = 1;
  pageSize = 6;

  resetParams() {
    return new ShopParams();
   }
}
