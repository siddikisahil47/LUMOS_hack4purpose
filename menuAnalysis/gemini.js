import {
    GoogleGenerativeAI,
    HarmCategory,
    HarmBlockThreshold,
} from "@google/generative-ai";
import fetch from 'node-fetch';
import { config } from 'dotenv';
config();

async function getRemoteData() {
    try {
        const response = await fetch('https://twis.in/shop/test/user.json');
        const data = await response.json();
        console.log("Remote data fetched successfully:", data);
        return data;
    } catch (error) {
        console.error("Error fetching remote data:", error);
        throw error;
    }
}

const GEMINI_API_KEY = process.env.GEMINI_API_KEY;
const genAI = new GoogleGenerativeAI(GEMINI_API_KEY);

const generationConfig = {
    temperature: 0.4,
    topK: 32,
    topP: 1,
    maxOutputTokens: 1056,
};

const safetySettings = [
    {
        category: HarmCategory.HARM_CATEGORY_HARASSMENT,
        threshold: HarmBlockThreshold.BLOCK_NONE,
    },
    {
        category: HarmCategory.HARM_CATEGORY_HATE_SPEECH,
        threshold: HarmBlockThreshold.BLOCK_NONE,
    },
    {
        category: HarmCategory.HARM_CATEGORY_SEXUALLY_EXPLICIT,
        threshold: HarmBlockThreshold.BLOCK_NONE,
    },
    {
        category: HarmCategory.HARM_CATEGORY_DANGEROUS_CONTENT,
        threshold: HarmBlockThreshold.BLOCK_NONE,
    },
];

const model = genAI.getGenerativeModel({ model: "gemini-1.0-pro-vision-latest", generationConfig, safetySettings });

export async function generateContent() {
    try {
        const data2 = await getRemoteData();

        // Accessing the first element of the array (assuming there's only one item in the array)
        const userData = data2[0];
        // Accessing the medical conditions from the nested object
        const md = userData['My Medical Conditions are'];
        console.log("Medical conditions fetched:", md);

        console.log("Starting content generation...");

        // Fetch the image URL from the JSON API
        const jsonUrl = "https://twis.in/shop/menu-test/img/image_data.json";
        const jsonResponse = await fetch(jsonUrl);
        const jsonData = await jsonResponse.json();
        const imagepath = jsonData.image_url;


        const response = await fetch(imagepath);
        const arrayBuffer = await response.arrayBuffer();
        const imageformat = Buffer.from(arrayBuffer).toString("base64");

        const parts = [
            { text: "you are a helpful Health food assistant. You know that which food is healthy and which food is unhealthy. Your task is to tell me what to eat based on my medical conditions. you should choice from these menu image and tell me only which food is healthy and which food is unhealthy." + md + "now tell me Which Food is healthy and which food is unhealthier always remember my medicial conditions and also tell me what to eat now."  },
            {
                inlineData: {
                    mimeType: "image/jpeg",
                    data: imageformat
                }
            }
        ];

        const data = await model.generateContent({ contents: [{ role: "user", parts }] });
        const result = await data.response;
        const text = await result.text();
        console.log('Generated text:', text);
        return text;
    } catch (error) {
        console.error("Error generating", error);
        throw error;
    }
}

export async function postData(text) {
    const response = await fetch('https://twis.in/shop/menu-test/response.php', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ text })
    });

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json();
    return data;
}

// Usage:
generateContent()
    .then(text => postData(text))
    .then(data => console.log(data))
    .catch(error => console.log('There was an error!', error));
