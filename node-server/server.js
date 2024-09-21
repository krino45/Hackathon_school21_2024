const express = require('express');
const cors = require('cors');
const mongoose = require('mongoose');
const app = express();
const port = 5258;

app.use(cors());
app.use(express.json());

const mongoURI = `mongodb+srv://${process.USER_LOGIN}:${process.USER_PASSWORD}@cluster0.08fqk.mongodb.net/`;

mongoose.connect(mongoURI, { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => console.log('Connected to MongoDB'))
  .catch((err) => console.error('Error connecting to MongoDB:', err));

app.post('/login', (req, res) => {
    const {login, password} = req.body;

    console.log(login);
});

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});