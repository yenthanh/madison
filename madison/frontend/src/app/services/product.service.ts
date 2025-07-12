import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, UpdateProductDescriptionDto } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5042/api/products';

  constructor(private http: HttpClient) { }

  getActiveProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/active`);
  }

  getDangerousDrugs(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/dangerous-drugs`);
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  updateProductDescription(updateDto: UpdateProductDescriptionDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-description`, updateDto);
  }
}
