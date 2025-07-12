import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product, ProductListResponse } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductCacheService {
  private productsCache = new BehaviorSubject<ProductListResponse | null>(null);
  private productDetailCache = new Map<number, Product>();
  private lastFetchTime = 0;
  private readonly CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

  constructor() {}

  setProducts(data: ProductListResponse): void {
    this.productsCache.next(data);
    this.lastFetchTime = Date.now();
  }

  getProducts(): Observable<ProductListResponse | null> {
    return this.productsCache.asObservable();
  }

  setProductDetail(product: Product): void {
    this.productDetailCache.set(product.id, product);
  }

  getProductDetail(id: number): Product | undefined {
    return this.productDetailCache.get(id);
  }

  isCacheValid(): boolean {
    return Date.now() - this.lastFetchTime < this.CACHE_DURATION;
  }

  clearCache(): void {
    this.productsCache.next(null);
    this.productDetailCache.clear();
    this.lastFetchTime = 0;
  }

  updateProductInCache(updatedProduct: Product): void {
    // Update in detail cache
    this.productDetailCache.set(updatedProduct.id, updatedProduct);
    
    // Update in list cache
    const currentList = this.productsCache.value;
    if (currentList) {
      const updatedProducts = currentList.products.map(p => 
        p.id === updatedProduct.id ? updatedProduct : p
      );
      this.productsCache.next({
        ...currentList,
        products: updatedProducts
      });
    }
  }
} 
