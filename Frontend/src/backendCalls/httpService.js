import axios from 'axios'
import logger from './logService'

axios.interceptors.response.use(null, (error) => {
  const expectedError = error.response && error.response.status >= 400 && error.response.status < 500

  if (!expectedError) {
    logger.log(error)
  }

  return Promise.reject(error)
})

const exportObject = {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete,
}
export default exportObject
