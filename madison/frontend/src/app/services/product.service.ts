import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, switchMap } from 'rxjs';
import { Product, UpdateProductDescriptionDto, ProductListResponse } from '../models/product';
import { ProductCacheService } from './product-cache.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5042/api/products';

  constructor(
    private http: HttpClient,
    private cacheService: ProductCacheService
  ) { }

  getActiveProducts(page: number = 1, pageSize: number = 20): Observable<ProductListResponse> {
    // Check cache first
    if (this.cacheService.isCacheValid()) {
      const cachedData = this.cacheService.getProducts();
      return cachedData.pipe(
        switchMap(data => {
          if (data) {
            return of(data);
          }
          return this.fetchActiveProducts(page, pageSize);
        })
      );
    }
    
    return this.fetchActiveProducts(page, pageSize);
  }

  private fetchActiveProducts(page: number, pageSize: number): Observable<ProductListResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    
    return this.http.get<ProductListResponse>(`${this.apiUrl}/active`, { params });
  }

  getDangerousDrugs(page: number = 1, pageSize: number = 20): Observable<ProductListResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    
    return this.http.get<ProductListResponse>(`${this.apiUrl}/dangerous-drugs`, { params });
  }

  getProduct(id: number): Observable<Product> {
    // Check cache first
    const cachedProduct = this.cacheService.getProductDetail(id);
    if (cachedProduct) {
      return of(cachedProduct);
    }
    
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  updateProductDescription(updateDto: UpdateProductDescriptionDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-description`, updateDto);
  }

  // Method to update cache after successful update
  updateProductInCache(updatedProduct: Product): void {
    this.cacheService.updateProductInCache(updatedProduct);
  }
}
