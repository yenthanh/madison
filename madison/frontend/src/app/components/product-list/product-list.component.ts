import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { ProductCacheService } from '../../services/product-cache.service';
import { ToastService } from '../../services/toast.service';
import { Product, ProductListResponse } from '../../models/product';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = false;
  error = '';
  currentPage = 1;
  pageSize = 20;
  totalItems = 0;
  totalPages = 0;
  activeTab = 'active'; // 'active' or 'dangerous'
  Math = Math; // For template usage

  constructor(
    private productService: ProductService,
    private cacheService: ProductCacheService,
    private toastService: ToastService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.checkForToastMessage();
  }

  private checkForToastMessage(): void {
    this.route.queryParams.subscribe(params => {
      const message = params['message'];
      const type = params['type'];
      
      if (message && type) {
        if (type === 'success') {
          this.toastService.showSuccess(message);
        } else if (type === 'error') {
          this.toastService.showError(message);
        }
      }
    });
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    // Check cache first for active products
    if (this.activeTab === 'active') {
      this.cacheService.getProducts().subscribe(cachedData => {
        if (cachedData && this.cacheService.isCacheValid()) {
          this.displayProducts(cachedData);
          this.loading = false;
        } else {
          this.fetchFromAPI();
        }
      });
    } else {
      this.fetchFromAPI();
    }
  }

  private fetchFromAPI(): void {
    const observable = this.activeTab === 'active' 
      ? this.productService.getActiveProducts(this.currentPage, this.pageSize)
      : this.productService.getDangerousDrugs(this.currentPage, this.pageSize);

    observable.subscribe({
      next: (response: ProductListResponse) => {
        this.displayProducts(response);
        
        // Cache the data if it's active products
        if (this.activeTab === 'active') {
          this.cacheService.setProducts(response);
        }
        
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Error loading products: ' + error.message;
        this.loading = false;
      }
    });
  }

  private displayProducts(response: ProductListResponse): void {
    this.products = response.products;
    this.totalItems = response.totalCount;
    this.totalPages = response.totalPages;
    this.currentPage = response.pageNumber;
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProducts();
  }

  onTabChange(tab: string): void {
    this.activeTab = tab;
    this.currentPage = 1;
    this.loadProducts();
  }

  getPageNumbers(): number[] {
    const pages: number[] = [];
    const start = Math.max(1, this.currentPage - 2);
    const end = Math.min(this.totalPages, this.currentPage + 2);
    
    for (let i = start; i <= end; i++) {
      pages.push(i);
    }
    return pages;
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 2
    }).format(price);
  }
}
