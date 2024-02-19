#!/bin/bash

# Navigate to your project directory
cd /frontend/ng-notes-app

# Function to install dependencies and build the Angular app
setup_app() {
  echo "Installing dependencies..."
  npm install

  echo "Building the Angular app..."
  ng build
}

# Function to start the Angular development server
start_app() {
  echo "Starting the Angular app..."
  ng serve
}

# Main script execution
main() {
  # Check if npm is installed
  if ! command -v npm &> /dev/null; then
    echo "Error: npm is not installed. Please install Node.js and npm before running this script."
    exit 1
  fi

  # Check if Angular CLI is installed
  if ! command -v ng &> /dev/null; then
    echo "Installing Angular CLI globally..."
    npm install -g @angular/cli
  fi

  # Check if the 'node_modules' directory exists, and run setup if not
  if [ ! -d "node_modules" ]; then
    setup_app
  fi

  # Start the Angular app
  start_app
}

# Run the main function
main
