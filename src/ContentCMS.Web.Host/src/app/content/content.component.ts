import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ActivatedRoute } from '@angular/router';
import { ContentDetailDto, ContentServiceProxy } from '@shared/service-proxies/service-proxies';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.css'],
  animations: [appModuleAnimation()]
})
export class ContentComponent extends AppComponentBase implements OnInit {

  content: ContentDetailDto;

  constructor(
    injector: Injector,
    private route: ActivatedRoute,
    private _contentService: ContentServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(
        switchMap(params => {
          const id = +params.get('id');;
          return this._contentService.getCMSContent(id)
        })
      )
      .subscribe(content => {
        this.content = content;
      });
  }

}
