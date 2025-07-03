import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'chat',
    loadChildren: () =>
      import('./chat/chat/chat.module').then((m) => m.ChatModule),
  },
  // Add more plugin routes here
  { path: '', redirectTo: 'chat', pathMatch: 'full' },
];
