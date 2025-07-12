import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, UpdateProductDescriptionDto, ProductListResponse } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5042/api/products';

  constructor(private http: HttpClient) { }

  getActiveProducts(page: number = 1, pageSize: number = 20): Observable<ProductListResponse> {
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
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  updateProductDescription(updateDto: UpdateProductDescriptionDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-description`, updateDto);
  }
}
