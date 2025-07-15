import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product, ProductListResponse } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductCacheService {
  private activeProductsCache = new BehaviorSubject<ProductListResponse | null>(null);
  private dangerousDrugsCache = new BehaviorSubject<ProductListResponse | null>(null);
  private productDetailCache = new Map<number, Product>();
  private activeProductsLastFetchTime = 0;
  private dangerousDrugsLastFetchTime = 0;
  private readonly CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

  constructor() {}

  // Active Products Cache
  setActiveProducts(data: ProductListResponse): void {
    this.activeProductsCache.next(data);
    this.activeProductsLastFetchTime = Date.now();
  }

  getActiveProducts(): Observable<ProductListResponse | null> {
    return this.activeProductsCache.asObservable();
  }

  isActiveProductsCacheValid(): boolean {
    return Date.now() - this.activeProductsLastFetchTime < this.CACHE_DURATION;
  }

  // Dangerous Drugs Cache
  setDangerousDrugs(data: ProductListResponse): void {
    this.dangerousDrugsCache.next(data);
    this.dangerousDrugsLastFetchTime = Date.now();
  }

  getDangerousDrugs(): Observable<ProductListResponse | null> {
    return this.dangerousDrugsCache.asObservable();
  }

  isDangerousDrugsCacheValid(): boolean {
    return Date.now() - this.dangerousDrugsLastFetchTime < this.CACHE_DURATION;
  }

  // Product Detail Cache
  setProductDetail(product: Product): void {
    this.productDetailCache.set(product.id, product);
  }

  getProductDetail(id: number): Product | undefined {
    return this.productDetailCache.get(id);
  }

  // Legacy methods for backward compatibility
  setProducts(data: ProductListResponse): void {
    this.setActiveProducts(data);
  }

  getProducts(): Observable<ProductListResponse | null> {
    return this.getActiveProducts();
  }

  isCacheValid(): boolean {
    return this.isActiveProductsCacheValid();
  }

  clearCache(): void {
    this.activeProductsCache.next(null);
    this.dangerousDrugsCache.next(null);
    this.productDetailCache.clear();
    this.activeProductsLastFetchTime = 0;
    this.dangerousDrugsLastFetchTime = 0;
  }

  updateProductInCache(updatedProduct: Product): void {
    // Update in detail cache
    this.productDetailCache.set(updatedProduct.id, updatedProduct);
    
    // Update in active products cache
    const currentActiveList = this.activeProductsCache.value;
    if (currentActiveList) {
      const updatedActiveProducts = currentActiveList.products.map(p => 
        p.id === updatedProduct.id ? updatedProduct : p
      );
      this.activeProductsCache.next({
        ...currentActiveList,
        products: updatedActiveProducts
      });
    }

    // Update in dangerous drugs cache
    const currentDangerousList = this.dangerousDrugsCache.value;
    if (currentDangerousList) {
      const updatedDangerousProducts = currentDangerousList.products.map(p => 
        p.id === updatedProduct.id ? updatedProduct : p
      );
      this.dangerousDrugsCache.next({
        ...currentDangerousList,
        products: updatedDangerousProducts
      });
    }
  }
} 
