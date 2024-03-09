# ToDoService

## Получить список задач:
```text
curl --location 'http://localhost:5105/tasks'
```
## Получить информацию о задаче по id:
```text
curl --location 'http://localhost:5105/tasks/5'
```
## Создание задачи:
```text
curl --location 'http://localhost:5105/tasks' \
--header 'Content-Type: application/json' \
--data '{
"id": 5,
"name": "задание-5",
"description": "апавпавпап",
"createdDate": "2024-03-08T00:00:00",
"deadlineDate": "2024-03-09T00:00:00",
"isCompleted": false
}'
```
## Удалить задачу:
```text
curl --location --request DELETE 'http://localhost:5105/tasks/5'
```