import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product, UpdateProductDescriptionDto } from '../../models/product';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css'
})
export class ProductFormComponent implements OnInit {
  product: Product | null = null;
  newDescription: string = '';
  loading = false;
  submitting = false;
  error = '';
  success = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.loadProduct(id);
      } else {
        this.error = 'Không tìm thấy ID sản phẩm';
      }
    });
  }

  loadProduct(id: number): void {
    this.loading = true;
    this.error = '';

    this.productService.getProduct(id).subscribe({
      next: (product) => {
        this.product = product;
        this.newDescription = product.description;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Lỗi khi tải thông tin sản phẩm: ' + error.message;
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (!this.product || !this.newDescription.trim()) {
      this.error = 'Vui lòng nhập mô tả mới';
      return;
    }

    this.submitting = true;
    this.error = '';
    this.success = '';

    const updateDto: UpdateProductDescriptionDto = {
      productId: this.product.id,
      description: this.newDescription.trim()
    };

    this.productService.updateProductDescription(updateDto).subscribe({
      next: (response) => {
        this.success = 'Cập nhật mô tả thành công!';
        this.submitting = false;
        
        // Redirect to product detail after 2 seconds
        setTimeout(() => {
          this.router.navigate(['/products', this.product!.id]);
        }, 2000);
      },
      error: (error) => {
        this.error = 'Lỗi khi cập nhật mô tả: ' + error.message;
        this.submitting = false;
      }
    });
  }

  goBack(): void {
    if (this.product) {
      this.router.navigate(['/products', this.product.id]);
    } else {
      this.router.navigate(['/products']);
    }
  }

  cancel(): void {
    this.goBack();
  }
}
