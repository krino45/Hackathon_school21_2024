const express = require('express');
const cors = require('cors');
const axios = require('axios');
const app = express();
const port = 8080;

app.use(cors());

app.get('/api/data', async (req, res) => {
  try {
    const response = await axios.get('http://localhost:5258/message');
    res.json({ message: response.data });
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Error fetching data from C# API' });
  }
});


app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
