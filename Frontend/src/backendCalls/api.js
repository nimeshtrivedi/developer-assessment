const toDoApiUri = 'https://localhost:44397/api/todoitems'

export async function getAllToDoItems() {
  const response = await fetch(`${toDoApiUri}`)
  const data = await response.json()

  if (!response.ok) {
    throw new Error(data.message || 'Could not fetch todo items.')
  }

  return data
}

export async function addToDoTask(taskData) {
  const response = await fetch(`${toDoApiUri}`, {
    method: 'POST',
    body: JSON.stringify(taskData),
    headers: {
      'Content-Type': 'application/json',
    },
  })
  const data = await response.json()

  if (!response.ok) {
    throw new Error(data.message || 'Could not create todo item.')
  }

  return null
}

export async function completeToDoTask(taskData) {
  taskData.isCompleted = true
  //   console.log('marking this task as completed', JSON.stringify(taskData.id, taskData))
  const response = await fetch(`${toDoApiUri}/${taskData.id}`, {
    method: 'PUT',
    body: JSON.stringify(taskData),
    headers: {
      'Content-Type': 'application/json',
    },
  })
  const data = await response.json()

  if (!response.ok) {
    throw new Error(data.message || 'Could not mark todo item.')
  }

  return null
}
