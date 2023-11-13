# ReflectForm - Dynamic Forms JSON for PWA Frameworks

Simple Question: Why should the front-end reimplement data types / validation when it already exists on the back-end? 

## Overview
This project is a proof of concept for generating JSON based form structures to be consumed by Progressive Web App (PWA) frameworks, utilizing C# and reflection to dynamically create forms. It's designed to introspect class details and convert them into JSON format, enabling the dynamic generation of forms in the front-end.

## Features
- **Reflection-Based**: Leverages C# reflection to introspect class properties.
- **JSON Conversion**: Converts class details into a JSON structure for easy use in front-end frameworks.
- **Custom Attributes**: Supports custom attributes to enhance functionality, such as labels and validation rules.
- **Nested Structures and Lists**: Capable of handling complex data structures, including nested objects and lists.

## Use Case
Ideal for projects requiring dynamic form generation based on backend models. Reduces the need for manual form updates, ensuring front-end forms stay in sync with backend data structures.

## Current Status
This project is a work in progress and was never intended to be used in production. Future updates may include enhanced validation, improved attribute handling, and broader framework compatibility.
