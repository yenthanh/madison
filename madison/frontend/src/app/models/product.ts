export interface Product {
  id: number;
  name: string;
  description: string;
  category: string;
  price: number;
  isActive: boolean;
  isDeleted: boolean;
  isDangerousDrug: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface UpdateProductDescriptionDto {
  productId: number;
  description: string;
} 
