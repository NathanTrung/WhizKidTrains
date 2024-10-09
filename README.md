**WhizKidTrains**

WhizKidTrains is an educational web application specifically designed for autistic children, offering a welcoming and structured environment where they can engage in interactive learning. Acting as a central hub, the platform connects several mini-games that teach essential concepts in rail transportation and logistics, all tailored to the needs of neurodiverse learners.

Developed by Team i20 for Dr. James George Marshall, WhizKidTrains combines sensory-friendly gameplay and educational content to provide an enriching and engaging experience that helps children develop cognitive and problem-solving skills in a fun and interactive way.


**Overview**

WhizKidTrains is an educational platform designed specifically for autistic children, providing an interactive and structured learning environment. Currently, the game features one mini-game that introduces children to the fundamentals of rail transportation and logistics through fun, sensory-friendly gameplay.

WhizKidTrains serves as a hub for future mini-games, with the platform ready to expand as more games are added. Each game will be tailored to accommodate neurodiverse learning styles, with accessible designs, clear guidance, and progressive difficulty to support learning and engagement.


**Features**

One Mini-Game Available: Currently, WhizKidTrains offers a single mini-game that introduces children to rail transportation concepts through engaging and accessible gameplay. This game allows players to explore the basics of train operations in a way that’s easy to understand and fun to interact with.

Hub for Future Mini-Games: The platform is designed to serve as a hub, ready to incorporate additional mini-games as they become available. Each future game will build on the foundation of interactive learning, helping children expand their knowledge of logistics and transportation in a safe and structured environment.

Sensory-Friendly Design: WhizKidTrains is tailored for autistic children, offering a calm and intuitive interface that minimizes distractions and supports a focused learning experience.

Interactive Learning: The game promotes active engagement by allowing players to control elements of gameplay, encouraging problem-solving, decision-making, and hands-on learning.

Educational Content: Through fun activities, children learn real-world concepts about how trains work, the roles they play in logistics, and the basics of transportation systems.

    
**Deployment Guide**

Welcome to the WhizKidTrains developer documentation! This guide will help you set up the project locally, understand its structure, and contribute effectively.
Prerequisites

Before you begin, ensure you have the following installed on your machine:
Node.js: Version 16.x or above
npm: Comes bundled with Node.js
Git: For version control
Firebase CLI: For deployment and backend services

Installation

Clone the Repository

    git clone https://github.com/NathanTrung/WhizKidTrains.git

Navigate to the Project Directory

    cd WhizKidTrains

Install Dependencies

    npm install

Set Up Environment Variables

Create a .env file in the root directory and add the following variables:

env

    REACT_APP_FIREBASE_API_KEY=your_firebase_api_key
    REACT_APP_FIREBASE_AUTH_DOMAIN=your_firebase_auth_domain
    REACT_APP_FIREBASE_PROJECT_ID=your_firebase_project_id
    REACT_APP_FIREBASE_STORAGE_BUCKET=your_firebase_storage_bucket
    REACT_APP_FIREBASE_MESSAGING_SENDER_ID=your_firebase_messaging_sender_id
    REACT_APP_FIREBASE_APP_ID=your_firebase_app_id

Note: Replace the placeholder values with your actual Firebase project credentials.

Project Structure

    graphql

    WhizKidTrains/
    │
    ├── public/                 # Public assets
    │   ├── index.html          # Main HTML file
    │   └── assets/             # Images, icons, etc.
    │
    ├── src/                    # Source code
    │   ├── components/         # Reusable React components
    │   ├── pages/              # Page components (e.g., Home, Games, Profile)
    │   ├── services/           # Firebase and API services
    │   ├── styles/             # CSS/SASS files
    │   ├── utils/              # Utility functions
    │   ├── App.js              # Main App component
    │   ├── index.js            # Entry point
    │   └── routes/             # Route definitions
    │
    ├── .gitignore              # Git ignore rules
    ├── package.json            # Project metadata and dependencies
    ├── README.md               # This file
    └── firebase.json           # Firebase configuration

Technologies Used

React: Front-end library for building user interfaces.
Firebase: Backend services including authentication, Firestore database, and hosting.
React Router: For client-side routing.
Redux: State management (if applicable).
SASS/SCSS: Enhanced CSS for styling.
Jest & React Testing Library: For unit and integration testing.
ESLint & Prettier: Code linting and formatting.
GitHub Actions: Continuous Integration and Deployment (CI/CD) workflows.

Running Locally

Start the Development Server

    npm start

Access the Application

Open your browser and navigate to http://localhost:3000.

Building for Production

Create an Optimized Build

    npm run build

This will generate a build/ directory with optimized production-ready files.

Deploy to Firebase

Ensure you have Firebase CLI installed and configured.

    firebase deploy

Note: This command deploys both the frontend and backend (if applicable) based on your firebase.json configuration.

Testing

Run the test suite using:

    npm test

This will execute all tests using Jest and React Testing Library.
Deployment

WhizKidTrains is deployed using Firebase Hosting. To deploy updates:

Login to Firebase

    firebase login

Deploy the Application

    firebase deploy

Note: This will upload your latest build to Firebase Hosting.


**Contributing**

We welcome contributions from the community! To contribute:

Fork the Repository

Click the "Fork" button at the top-right corner of the repository page.

Clone Your Fork

    git clone https://github.com/your-username/WhizKidTrains.git

Create a Feature Branch

    git checkout -b feature/your-feature-name

Commit Your Changes

    git commit -m "Add feature: your feature description"

Push to Your Fork

    git push origin feature/your-feature-name

Open a Pull Request

Navigate to the original repository and open a pull request from your fork.

Guidelines:

Follow the existing code style and structure.
Ensure all tests pass before submitting.
Provide clear and descriptive commit messages.
Include documentation for new features or changes.

        
**Live Demo**

Experience WhizKidTrains firsthand by visiting the live prototype:

🔗 [WhizKidTrains Prototype](https://whizkidtrains-proto.web.app/)


**Client Guide**

Accessing WhizKidTrains

Web Application: Access the application directly through your web browser at WhizKidTrains Prototype.
Supported Browsers: Compatible with the latest versions of Chrome, Firefox, Safari, and Edge.
Devices: Optimized for both desktop and mobile devices to ensure a seamless experience.

Using the Application

Sign Up / Log In:
    New users can create an account using their email or via social logins (Google, Facebook).
    Returning users can log in with their credentials.

Dashboard:
    After logging in, you'll land on the dashboard where you can choose between different games and educational modules.

Games:
        Train Rider: Ride train to different stations that house different mini-games.
        Game Manager: Select from available mini-games.

Support

If you encounter any issues or have questions, please reach out to our support team:

Email: nathantrung5@gmail.com
GitHub Issues: Open an Issue
