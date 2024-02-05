import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment.prod';

import { webSocket } from 'rxjs/webSocket';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

document.addEventListener('DOMContentLoaded', () => {
  // Ініціалізація веб-сокета
  const socket$ = webSocket('wss://localhost:44405/ws');

  platformBrowserDynamic().bootstrapModule(AppModule)
    .catch(err => console.error(err));
});
platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
