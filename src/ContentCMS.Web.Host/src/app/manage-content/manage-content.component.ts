import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { 
  ContentServiceProxy, 
  UpsertContentInput,
  ContentDetailDto
 } from '@shared/service-proxies/service-proxies';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { MenuListReactiveService } from '@shared/reactive/menu-list.reactive.service';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
  selector: 'app-content',
  templateUrl: './manage-content.component.html',
  styleUrls: ['./manage-content.component.css'],
  animations: [appModuleAnimation()]
})

export class ManageContentComponent extends AppComponentBase implements OnInit {

  saving = false;
  content: UpsertContentInput = new UpsertContentInput();
  contentList: ContentDetailDto[];
  editorConfig: AngularEditorConfig = {
    editable: true,
    height: 'auto',
    minHeight: '25vh',
    maxHeight: '40vh',
    width: 'auto',
    minWidth: '0',
    enableToolbar: true,
    showToolbar: true,
    sanitize: false
  };

  constructor(
    injector: Injector,
    private _contentService: ContentServiceProxy,
    private menuListReactiveService: MenuListReactiveService
  ) { 
    super(injector);
  }

  ngOnInit(): void {
    this._contentService.getAll().subscribe(contents => {
      this.contentList = contents.items;
    })
  }

  contentSelected(itemSelectedId: number) {
    this.content.init(this.contentList.find(x => x.id === itemSelectedId));
  }

  save(): void {
    this.saving = true;
    if(!this.content.id)
      this.content.id = 0;
      
    this._contentService
      .insertOrUpdateCMSContent(this.content)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((reponse) => {
        this.content.id === 0
          ? this.addContentItem(reponse)
          : this.updateContentItem(reponse);

        this.notify.info(this.l('SavedSuccessfully'));
      });
  }

  private updateContentItem(updatedItem: ContentDetailDto) {
    const index = this.contentList.findIndex(x => x.id === updatedItem.id);

    if(index)
      this.contentList[index] = updatedItem;
  }

  private addContentItem(newItem: ContentDetailDto) {
    this.contentList.push(newItem);
    this.content = newItem;

    //Add new item to menu
    const newMenuItem = new MenuItem(`${newItem.pageName}`, '', "chevron_right", `/app/content/${newItem.id}`);
    this.menuListReactiveService.menuItem.next(newMenuItem);
  }
}
