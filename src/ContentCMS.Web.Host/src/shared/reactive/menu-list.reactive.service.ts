import { Injectable } from '@angular/core';
import { MenuItem } from '@shared/layout/menu-item';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class MenuListReactiveService {
  menuItem: BehaviorSubject<MenuItem> = new BehaviorSubject<MenuItem>(null);;
}
