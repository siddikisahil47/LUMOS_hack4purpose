import express from 'express';
import { generateContent, postData } from './gemini.js';

const app = express()
const PORT = 5000

// Middleware to set headers
app.use((req, res, next) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Content-Type', 'application/json');
    next();
});

app.get('/', async (req, res) => {
    try {
        const text = await generateContent();
        const data = await postData(text);
        res.json(data);
    } catch (error) {
        res.status(500).json({ error: 'There was an error generating content', message: error.message });
    }
});


app.listen(PORT, () => {
    console.log(`Server running at ${PORT}`);
});