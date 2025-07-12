import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product, ProductListResponse } from '../../models/product';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {
  activeProducts: Product[] = [];
  dangerousDrugs: Product[] = [];
  loading = false;
  error = '';

  // Pagination for active products
  activeProductsPage = 1;
  activeProductsPageSize = 20;
  activeProductsTotalCount = 0;
  activeProductsTotalPages = 0;

  // Pagination for dangerous drugs
  dangerousDrugsPage = 1;
  dangerousDrugsPageSize = 20;
  dangerousDrugsTotalCount = 0;
  dangerousDrugsTotalPages = 0;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    // Load active products
    this.productService.getActiveProducts(this.activeProductsPage, this.activeProductsPageSize).subscribe({
      next: (response: ProductListResponse) => {
        this.activeProducts = response.products;
        this.activeProductsTotalCount = response.totalCount;
        this.activeProductsTotalPages = response.totalPages;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Error loading product list: ' + error.message;
        this.loading = false;
      }
    });

    // Load dangerous drugs
    this.productService.getDangerousDrugs(this.dangerousDrugsPage, this.dangerousDrugsPageSize).subscribe({
      next: (response: ProductListResponse) => {
        this.dangerousDrugs = response.products;
        this.dangerousDrugsTotalCount = response.totalCount;
        this.dangerousDrugsTotalPages = response.totalPages;
      },
      error: (error) => {
        console.error('Error loading dangerous drugs:', error);
      }
    });
  }

  // Pagination methods for active products
  loadActiveProductsPage(page: number): void {
    if (page >= 1 && page <= this.activeProductsTotalPages) {
      this.activeProductsPage = page;
      this.productService.getActiveProducts(this.activeProductsPage, this.activeProductsPageSize).subscribe({
        next: (response: ProductListResponse) => {
          this.activeProducts = response.products;
          this.activeProductsTotalCount = response.totalCount;
          this.activeProductsTotalPages = response.totalPages;
        },
        error: (error) => {
          this.error = 'Error loading product list: ' + error.message;
        }
      });
    }
  }

  // Pagination methods for dangerous drugs
  loadDangerousDrugsPage(page: number): void {
    if (page >= 1 && page <= this.dangerousDrugsTotalPages) {
      this.dangerousDrugsPage = page;
      this.productService.getDangerousDrugs(this.dangerousDrugsPage, this.dangerousDrugsPageSize).subscribe({
        next: (response: ProductListResponse) => {
          this.dangerousDrugs = response.products;
          this.dangerousDrugsTotalCount = response.totalCount;
          this.dangerousDrugsTotalPages = response.totalPages;
        },
        error: (error) => {
          console.error('Error loading dangerous drugs:', error);
        }
      });
    }
  }

  getStatusClass(product: Product): string {
    if (product.isDangerousDrug) {
      return 'dangerous';
    }
    return product.isActive ? 'active' : 'inactive';
  }

  // Helper methods for pagination
  getActiveProductsPageNumbers(): number[] {
    return this.getPageNumbers(this.activeProductsPage, this.activeProductsTotalPages);
  }

  getDangerousDrugsPageNumbers(): number[] {
    return this.getPageNumbers(this.dangerousDrugsPage, this.dangerousDrugsTotalPages);
  }

  private getPageNumbers(currentPage: number, totalPages: number): number[] {
    const pages: number[] = [];
    const maxVisiblePages = 5;
    
    if (totalPages <= maxVisiblePages) {
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
    } else {
      let start = Math.max(1, currentPage - 2);
      let end = Math.min(totalPages, start + maxVisiblePages - 1);
      
      if (end - start + 1 < maxVisiblePages) {
        start = Math.max(1, end - maxVisiblePages + 1);
      }
      
      for (let i = start; i <= end; i++) {
        pages.push(i);
      }
    }
    
    return pages;
  }
}
