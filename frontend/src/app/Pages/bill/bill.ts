import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BillModel } from '../../Class/bill-model';
import { CustomerService } from '../../Service/customer-service';
import { InventoryService } from '../../Service/inventory-service';
import { CustomerModel } from '../../Class/customer-model';
import { InventoryModel } from '../../Class/inventory-model';

@Component({
  selector: 'app-bill',
  imports: [FormsModule,CommonModule],
  templateUrl: './bill.html',
  styleUrl: './bill.css',
})
export class Bill {
     customers: CustomerModel = new CustomerModel();
     billList:BillModel[] = [];
     customerList:CustomerModel[] = [];
     productList:InventoryModel[] =[];
     bills: BillModel = new BillModel();
     products:InventoryModel = new InventoryModel();
     customerSrv = inject(CustomerService);
     inventorytSrv = inject(InventoryService);  
     totalAmount: number = 0;



     ngOnInit(){
      this.getBillByCustomer();
      this.getBillByProduct();
     }

     getBillByCustomer(){
      this.customerSrv.getAll().subscribe((res)=>{
        this.customerList = res;
      })
     } 

     getBillByProduct(){
      this.inventorytSrv.getAll().subscribe((res)=>{
        this.productList =res;
      })
     } 

     onProductChange(){
      const selectedProduct = this.productList.find(p=> p.productId === this.bills.productId);
      if(selectedProduct){
        this.bills.price = selectedProduct.price;
        this.calculateTotalAmount();
      }
    }
     calculateTotalAmount(){
      this.bills.amount = this.bills.quantity * this.bills.price;
     }   

     addItem(){
      this.calculateTotalAmount();
      this.billList.push({...this.bills});
      this.calculateGrandTotal();
      this.bills.productId =0;
      this.bills.quantity =0;
      this.bills.price =0;
      this.bills.amount =0;
     } 

     removeItem(index: number) {
  this.billList.splice(index, 1);
    this.calculateGrandTotal();
} 

calculateGrandTotal(){
  this.totalAmount =0;
  for(let item of this.billList){
    this.totalAmount += item.amount;
  }
}

     
}
