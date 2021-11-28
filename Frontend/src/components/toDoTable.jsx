import React, { useState } from 'react'
import { Button, Table } from 'react-bootstrap'
import { saveToDoItem, getToDoItems } from '../backendCalls/toDoService'

const ToDoTable = () => {
  const [items, setItems] = useState([])

  async function getItems() {
    try {
      const { data } = await getToDoItems()

      setItems(data)
    } catch (error) {
      console.error(error)
    }
  }

  async function handleMarkAsComplete(item) {
    try {
      item.isCompleted = true
      saveToDoItem(item)
      const filteredItems = items.filter((curItem) => {
        return curItem.id !== item.id
      })
      setItems(filteredItems)
    } catch (error) {
      console.error(error)
    }
  }

  return (
    <>
      <h1>
        Showing {items.length} Item(s){' '}
        <Button variant="primary" className="pull-right" onClick={() => getItems()}>
          Refresh
        </Button>
      </h1>

      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.description}</td>
              <td>
                <Button variant="warning" size="sm" onClick={() => handleMarkAsComplete(item)}>
                  Mark as completed
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}

export default ToDoTable
