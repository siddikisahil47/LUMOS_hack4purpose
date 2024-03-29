# Backend Server with Gemini Content Generation

This repository contains a backend server built with Node.js and Express.js that utilizes the Gemini API from Google Generative AI to generate content based on images fetched from an external API. The generated content is then posted to another endpoint.

## Installation

To install the necessary dependencies, run:

```
npm install
```

## Configuration

Ensure you have a `.env` file in the root directory with the following configuration:

```
GEMINI_API_KEY=your_gemini_api_key_here
```

## Usage

To start the server, run:

```
npm start
```

The server will run on port 4000 by default.

## Files Overview

### `gemini.js`

This file contains the logic for generating content using the Gemini API. It fetches an image URL from an external JSON API, retrieves the image, and generates text content based on the image using predefined safety settings.

### `index.js`

The main entry point for the Express server. It sets up routes and middleware for handling HTTP requests. The `/` route triggers content generation and posting.

### `package.json`

Contains metadata and dependencies for the Node.js project.

## Dependencies

- `@google/generative-ai`: Google's Generative AI library for content generation.
- `dotenv`: For loading environment variables from a `.env` file.
- `express`: Web framework for Node.js used to build the server.
- `node-fetch`: For making HTTP requests from the server.

## API Endpoints

- `GET /`: Triggers content generation, retrieves generated content, and posts it to another endpoint.

## Error Handling

If there's an error during content generation or posting, the server responds with a 500 status code and an error message.

Feel free to explore the codebase and contribute to enhance its functionality!