import { Component, OnInit } from '@angular/core';
import { TextService } from './text.service';

interface AboutText {
  text:string
}

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  displayText = '';
  editedText = ''; // поле для редагованого тексту
  isEditing = false; // флаг для визначення, чи відбувається редагування

  constructor(private textService: TextService) {}

  ngOnInit(): void {
    this.loadText();
  }

  loadText(): void {
    this.textService.getText().subscribe((response: AboutText) => {
      this.displayText = response.text;
      this.editedText = response.text;
    });
  }

  toggleEdit(): void {
    if (this.isEditing) {
      // Якщо ми в процесі редагування, збережіть редагований текст
      this.textService.setText(this.editedText).subscribe((response: AboutText) => {
        this.displayText = response.text;
      });
    } else {
      // Якщо починаємо редагування, скопіюйте текст для редагування
      this.editedText = this.displayText;
    }
    this.isEditing = !this.isEditing; // змініть флаг редагування
  }
}
