import http from './httpService'
import { apiUrl } from '../config.json'

const apiEndpoint = apiUrl + '/todoitems'

function toDoItemUrl(id) {
  return `${apiEndpoint}/${id}`
}

export const getToDoItem = (toDoItemId) => {
  return http.get(toDoItemUrl(toDoItemId))
}

export function saveToDoItem(toDoItem) {
  if (toDoItem.id) {
    const body = { ...toDoItem }

    return http.put(toDoItemUrl(toDoItem.id), body)
  }

  return http.post(apiEndpoint, toDoItem)
}

export const getToDoItems = () => http.get(apiEndpoint)
