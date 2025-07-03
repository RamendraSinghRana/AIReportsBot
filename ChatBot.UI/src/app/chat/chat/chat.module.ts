import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Chat } from './chat';
import { FormsModule } from '@angular/forms';
import { HttpClientModule  } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [Chat, CommonModule, FormsModule, HttpClientModule ],
  exports: [],
})
export class ChatModule {}