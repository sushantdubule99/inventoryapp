import { Routes } from '@angular/router';

import { Home } from './Pages/home/home';
import { Inventory } from './Pages/inventory/inventory';
import { Customer } from './Pages/customer/customer';
import { Bill } from './Pages/bill/bill';
import { Login } from './Pages/login/login';

import { authGuard } from './guards/auth-guard';
import { Layout } from './Pages/layout/layout';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login
  },

  {
    path: '',
    component: Layout,
    canActivate: [authGuard],
    children: [
      {
        path: 'home',
        component: Home
      },
      {
        path: 'inventory',
        component: Inventory
      },
      {
        path: 'customer',
        component: Customer
      },
      {
        path: 'bill',
        component: Bill
      }
    ]
  }

];