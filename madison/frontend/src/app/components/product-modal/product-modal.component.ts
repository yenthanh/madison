import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="modal-overlay" (click)="closeModal()">
      <div class="modal-content" (click)="$event.stopPropagation()">
        <div class="modal-header">
          <h2>{{ product?.name }}</h2>
          <button class="close-button" (click)="closeModal()">×</button>
        </div>
        
        <div class="modal-body">
          <div class="product-info">
            <div class="info-row">
              <span class="label">Description:</span>
              <span class="value">{{ product?.description }}</span>
            </div>
            
            <div class="info-row">
              <span class="label">Category:</span>
              <span class="value category-badge">{{ product?.category }}</span>
            </div>
            
            <div class="info-row">
              <span class="label">Price:</span>
              <span class="value price">{{ formatPrice(product?.price || 0) }}</span>
            </div>
            
            <div class="info-row">
              <span class="label">Status:</span>
              <span class="value status-badge" [ngClass]="product?.isActive ? 'active' : 'inactive'">
                {{ product?.isActive ? 'Active' : 'Inactive' }}
              </span>
            </div>
            
            <div class="info-row">
              <span class="label">Drug Type:</span>
              <span class="value status-badge" [ngClass]="product?.isDangerousDrug ? 'dangerous' : 'normal'">
                {{ product?.isDangerousDrug ? 'Dangerous Drug' : 'Normal Drug' }}
              </span>
            </div>
            
            <div class="info-row">
              <span class="label">Created Date:</span>
              <span class="value">{{ product?.createdAt | date:'dd/MM/yyyy HH:mm' }}</span>
            </div>
            
            <div class="info-row" *ngIf="product?.updatedAt">
              <span class="label">Last Updated:</span>
              <span class="value">{{ product?.updatedAt | date:'dd/MM/yyyy HH:mm' }}</span>
            </div>
          </div>
        </div>
        
        <div class="modal-footer">
          <button class="btn btn-secondary" (click)="closeModal()">Close</button>
          <button class="btn btn-primary" (click)="editProduct()">Edit Description</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .modal-overlay {
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      background: rgba(0, 0, 0, 0.5);
      display: flex;
      justify-content: center;
      align-items: center;
      z-index: 1000;
      animation: fadeIn 0.3s ease-out;
    }

    .modal-content {
      background: white;
      border-radius: 12px;
      box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.1);
      max-width: 600px;
      width: 90%;
      max-height: 90vh;
      overflow-y: auto;
      animation: slideIn 0.3s ease-out;
    }

    .modal-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 20px 24px;
      border-bottom: 1px solid #e2e8f0;
    }

    .modal-header h2 {
      margin: 0;
      color: #1e293b;
      font-size: 1.5em;
    }

    .close-button {
      background: none;
      border: none;
      font-size: 24px;
      cursor: pointer;
      color: #64748b;
      padding: 0;
      width: 32px;
      height: 32px;
      display: flex;
      align-items: center;
      justify-content: center;
      border-radius: 4px;
      transition: all 0.2s;
    }

    .close-button:hover {
      background: #f1f5f9;
      color: #374151;
    }

    .modal-body {
      padding: 24px;
    }

    .product-info {
      margin-bottom: 20px;
    }

    .info-row {
      display: flex;
      margin-bottom: 15px;
      align-items: center;
      gap: 15px;
    }

    .label {
      font-weight: 600;
      color: #374151;
      min-width: 120px;
      flex-shrink: 0;
    }

    .value {
      color: #6b7280;
      display: flex;
      align-items: center;
    }

    .category-badge {
      background: #e0f2fe;
      color: #0369a1;
      padding: 6px 12px;
      border-radius: 16px;
      font-size: 0.85em;
      font-weight: 500;
      display: inline-block;
      white-space: nowrap;
    }

    .price {
      font-weight: bold;
      color: #059669;
      font-size: 1.2em;
    }

    .status-badge {
      padding: 6px 12px;
      border-radius: 16px;
      font-size: 0.85em;
      font-weight: 500;
      display: inline-block;
      white-space: nowrap;
    }

    .status-badge.active {
      background: #dcfce7;
      color: #166534;
    }

    .status-badge.inactive {
      background: #f1f5f9;
      color: #64748b;
    }

    .status-badge.dangerous {
      background: #fef2f2;
      color: #dc2626;
    }

    .status-badge.normal {
      background: #dcfce7;
      color: #166534;
    }

    .modal-footer {
      display: flex;
      justify-content: flex-end;
      gap: 12px;
      padding: 20px 24px;
      border-top: 1px solid #e2e8f0;
    }

    .btn {
      padding: 10px 20px;
      border: none;
      border-radius: 8px;
      font-size: 14px;
      font-weight: 500;
      cursor: pointer;
      transition: all 0.2s;
      text-decoration: none;
      display: inline-flex;
      align-items: center;
      justify-content: center;
    }

    .btn-primary {
      background: #3498db;
      color: white;
    }

    .btn-primary:hover {
      background: #2980b9;
    }

    .btn-secondary {
      background: #f8f9fa;
      color: #495057;
      border: 1px solid #dee2e6;
    }

    .btn-secondary:hover {
      background: #e9ecef;
    }

    @keyframes fadeIn {
      from { opacity: 0; }
      to { opacity: 1; }
    }

    @keyframes slideIn {
      from {
        transform: translateY(-20px);
        opacity: 0;
      }
      to {
        transform: translateY(0);
        opacity: 1;
      }
    }

    @media (max-width: 768px) {
      .modal-content {
        width: 95%;
        margin: 20px;
      }
      
      .modal-header,
      .modal-body,
      .modal-footer {
        padding: 16px;
      }
      
      .info-row {
        flex-direction: column;
        align-items: flex-start;
        gap: 8px;
      }
      
      .label {
        min-width: auto;
      }
      
      .modal-footer {
        flex-direction: column;
      }
      
      .btn {
        width: 100%;
      }
    }
  `]
})
export class ProductModalComponent {
  @Input() product: Product | null = null;
  @Output() close = new EventEmitter<void>();
  @Output() edit = new EventEmitter<Product>();

  closeModal(): void {
    this.close.emit();
  }

  editProduct(): void {
    if (this.product) {
      this.edit.emit(this.product);
    }
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 2
    }).format(price);
  }
} 
