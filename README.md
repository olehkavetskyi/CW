# Курсова робота 
## Предмет
Конструювання програмного забезпечення
## Завдання

Варіант 10

Ваша фірма займається торгівлею промисловими товарами. У вашому розпорядженні є один
склад і 5 магазинів. Окрім того, організовано ще кілька виносних торгових місць на 1
людину в ряді чужих магазинів. Кожний магазин чи торгова точка може замовляти товари
на складі, однак при цьому несе відповідальність за неефективність замовлень – тобто,
повернення непроданого товару чи неефективне використання торгового місця карається.
Вам необхідно розв`язувати задачі обліку продажів по всіх магазинах і торгових місцях,
здійснювати аналіз ефективності продажів товарів і ефективності роботи магазинів та
окремих працівників.

## Використанні технології

1. ASP.NET 8.0
2. Angular 16.2.0

Деталі можна переглянути на GitHub  Insights  Dependency graph

## Інструкція для встановлення 

1. Склонуйте проєкт

```bash
httpsgithub.comolehkavetskyiCW.git 
```
2. Перейшовши в Angular проєкт можна ознайомитися з інструкціями в `README.md`.
3. Встановити додаткові бібліотеки в клієнті

```bash
npm i jwt-decode
```
```bash 
npm i ngx-toastr
```
```bash
npm i ngx-spinner
```
4. Встановити додаткові фреймворки для API
```bash
Microsoft.AspNetCore.Authentication.JwtBearer
```
```bash
Microsoft.AspNetCore.Identity.EntityFrameworkCore
```
```bash
Microsoft.EntityFrameworkCore.Tools
```
```bash
Moq
```
```bash
Newtonsoft.Json
```
```bash
Npgsql.EntityFrameworkCore.PostgreSQL
```
5. Додати власну строку підключення до бази даних. В цьому проєкті використовується Postgres, проте за потреби можна замінити провайдера ( `Npgsql.EntityFrameworkCore.PostgreSQL` )

Змініть строку в файлі appsettings.json
```json
  ConnectionStrings {
    DefaultConnection your_connection_string
  },
```