import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  displayText = `
    <h1>Генерація унікального ідентифікатора:</h1>
    <p>
      Використовується клас Guid для створення унікального ідентифікатора (GUID).
      Отриманий GUID перетворюється в масив байтів (byte[] bytes = Guid.NewGuid().ToByteArray();).
      Перетворення в рядок:
    </p>
    <p>
      Масив байтів перетворюється в рядок за допомогою методу Convert.ToBase64String(bytes).
      Результат цього перетворення буде довгою строкою, що містить символи base64.
      Форматування рядка:
    </p>
    <p>
      Заміна символів "/" на "_" за допомогою .Replace("/", "_").
      Заміна символів "+" на "-" за допомогою .Replace("+", "-").
      Вибір підстроки:
    </p>
    <p>
      Вибір підстроки з отформатованої строки, починаючи з позиції 0 і з довжиною 8 символів: .Substring(0, 8).
      Повернення результату:
    </p>
    <p>
      Отримана підстрока є скороченою версією вихідного посилання і повертається як результат методу ShortenLink.
    </p>
  `;
  editedText = ''; // поле для редагованого тексту
  isEditing = false; // флаг для визначення, чи відбувається редагування

  toggleEdit() {
    if (this.isEditing) {
      // Якщо ми в процесі редагування, збережіть редагований текст
      this.displayText = this.editedText;
    }
    this.isEditing = !this.isEditing; // змініть флаг редагування
  }
}
