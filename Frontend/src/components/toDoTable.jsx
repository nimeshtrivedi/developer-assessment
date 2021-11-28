import React, { useState, useEffect } from 'react'
import { Button, Table } from 'react-bootstrap'
import { getAllToDoItems, completeToDoTask } from '../backendCalls/api'
import useHttp from './../customHooks/serverCalls'

const ToDoTable = () => {
  const [items, setItems] = useState([])
  const { sendRequest, status, data, error } = useHttp(getAllToDoItems, true)
  const { sendRequest: markCompleted, completedError } = useHttp(completeToDoTask)

  async function getItems() {
    setItems([])
    if (status === 'completed') {
      console.log(data)
      setItems(data)
      console.log('fetch completed')
    }
    if (error) console.log(error)
    console.log('getitems clicked')
  }

  async function handleMarkAsComplete(item) {
    try {
      markCompleted(item)
      const filteredItems = items.filter((curItem) => {
        return curItem.id != item.id
      })
      setItems(filteredItems)
    } catch (error) {
      console.error(error)
    }
  }

  useEffect(() => {
    sendRequest()
  }, [sendRequest])

  useEffect(() => {
    markCompleted()
  }, [markCompleted])

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
