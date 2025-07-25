import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product, UpdateProductDescriptionDto } from '../../models/product';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css'
})
export class ProductFormComponent implements OnInit, OnDestroy {
  product: Product | null = null;
  newDescription: string = '';
  submitting = false;
  error = '';
  success = '';
  private routeSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    // Get product ID from route params
    this.routeSubscription = this.route.params.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.loadProduct(id);
      } else {
        this.error = 'Product ID not found';
      }
    });
  }

  ngOnDestroy(): void {
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
  }

  loadProduct(id: number): void {
    this.error = '';

    this.productService.getProduct(id).subscribe({
      next: (product) => {
        this.product = product;
        this.newDescription = product.description;
      },
      error: (error) => {
        this.error = 'Error loading product information: ' + error.message;
      }
    });
  }

  onSubmit(): void {
    if (!this.product || !this.newDescription.trim()) {
      this.error = 'Please enter a new description';
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
        this.submitting = false;
        
        // Update cache with new description
        if (this.product) {
          const updatedProduct = { ...this.product, description: this.newDescription.trim() };
          this.productService.updateProductInCache(updatedProduct);
        }
        
        // Redirect to product list immediately
        this.router.navigate(['/products'], { 
          queryParams: { 
            message: 'Description updated successfully!',
            type: 'success'
          }
        });
      },
      error: (error) => {
        this.error = 'Error updating description: ' + error.message;
        this.submitting = false;
      }
    });
  }

  goBack(): void {
    // Navigate back to products list without any query params
    this.router.navigate(['/products']);
  }

  cancel(): void {
    // Navigate back to products list without any query params
    this.router.navigate(['/products']);
  }
}
