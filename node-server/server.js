const express = require('express')
const cors = require('cors')
const mongoose = require('mongoose')
const bcrypt = require('bcrypt')
const app = express()
const port = 5258

app.use(cors());
app.use(express.json());

const mongoURI = `mongodb+srv://${process.env.USER_LOGIN}:${process.env.USER_PASSWORD}@cluster0.08fqk.mongodb.net/`;

const User = require('./schemes/User');

mongoose.connect(mongoURI, { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => console.log('Connected to MongoDB'))
  .catch((err) => console.error('Error connecting to MongoDB:', err, mongoURI));

app.post('/login', async (req, res) => {
  const {login, password} = req.body;

  try {
    let user = await User.findOne({login}); 

    if (!user) {
      return res.status(404).json({message: 'User not found'})
    }

    if (user.password !== password) {
      return res.status(404).json({message: 'Invalid password'})
    }

    return res.status(200).json({
      data: user,
      statusAuth: true
    })
  } catch(e) {
    console.log(e);
    return res.status(500).json({message: 'Server error'})
  }

});

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`)
});