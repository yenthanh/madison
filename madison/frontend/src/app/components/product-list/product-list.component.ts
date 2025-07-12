import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';

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

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    // Load active products
    this.productService.getActiveProducts().subscribe({
      next: (products) => {
        this.activeProducts = products;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Lỗi khi tải danh sách sản phẩm: ' + error.message;
        this.loading = false;
      }
    });

    // Load dangerous drugs
    this.productService.getDangerousDrugs().subscribe({
      next: (products) => {
        this.dangerousDrugs = products;
      },
      error: (error) => {
        console.error('Lỗi khi tải thuốc nguy hiểm:', error);
      }
    });
  }

  getStatusClass(product: Product): string {
    if (product.isDangerousDrug) {
      return 'dangerous';
    }
    return product.isActive ? 'active' : 'inactive';
  }
}
