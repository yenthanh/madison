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
    // Always fetch from API for now, cache will be handled in fetchActiveProducts
    return this.fetchActiveProducts(page, pageSize);
  }

  getDangerousDrugs(page: number = 1, pageSize: number = 20): Observable<ProductListResponse> {
    // Always fetch from API for now, cache will be handled in fetchDangerousDrugs
    return this.fetchDangerousDrugs(page, pageSize);
  }

  private fetchActiveProducts(page: number, pageSize: number): Observable<ProductListResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<ProductListResponse>(`${this.apiUrl}/active`, { params }).pipe(
      switchMap(data => {
        // Only cache page 1
        if (page === 1) {
          console.log('Caching active products page 1');
          this.cacheService.setActiveProducts(data);
        }
        return of(data);
      })
    );
  }

  private fetchDangerousDrugs(page: number, pageSize: number): Observable<ProductListResponse> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<ProductListResponse>(`${this.apiUrl}/dangerous-drugs`, { params }).pipe(
      switchMap(data => {
        // Only cache page 1
        if (page === 1) {
          console.log('Caching dangerous drugs page 1');
          this.cacheService.setDangerousDrugs(data);
        }
        return of(data);
      })
    );
  }

  getProduct(id: number): Observable<Product> {
    // Check detail cache first
    const cachedProduct = this.cacheService.getProductDetail(id);
    if (cachedProduct) {
      return of(cachedProduct);
    }

    return this.http.get<Product>(`${this.apiUrl}/${id}`).pipe(
      switchMap(product => {
        this.cacheService.setProductDetail(product);
        return of(product);
      })
    );
  }

  updateProductDescription(updateDto: UpdateProductDescriptionDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-description`, updateDto);
  }

  updateProductInCache(updatedProduct: Product): void {
    this.cacheService.updateProductInCache(updatedProduct);
  }
}
