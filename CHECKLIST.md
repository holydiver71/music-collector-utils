# Music Collection Data Processing Plan

## Project Overview
This project will extract and organize music collection data from the MusicCollectorzExport.xml file into structured JSON files. Each JSON file will contain a list of unique objects with assigned unique IDs for future reference. as each step is completed tick off the checklist.

## Phase 1: Extract Common Entity Lists

### ✅ Step 1: Artists JSON File
- **File**: `data/artists.json`
- **Source**: Extract from `<artist><displayname>` tags
- **Structure**: 
  ```json
  {
    "artists": [
      {
        "id": 1,
        "name": "AC/DC"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 2: Genres JSON File
- **File**: `data/genres.json`
- **Source**: Extract from `<genre><displayname>` tags
- **Structure**: 
  ```json
  {
    "genres": [
      {
        "id": 1,
        "name": "Hard Rock"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 3: Record Labels JSON File
- **File**: `data/labels.json`
- **Source**: Extract from `<label><displayname>` tags
- **Structure**: 
  ```json
  {
    "labels": [
      {
        "id": 1,
        "name": "Atlantic"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 4: Formats JSON File
- **File**: `data/formats.json`
- **Source**: Extract from `<format><displayname>` tags
- **Structure**: 
  ```json
  {
    "formats": [
      {
        "id": 1,
        "name": "LP"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 5: Packaging JSON File
- **File**: `data/packaging.json`
- **Source**: Extract from `<packaging><displayname>` tags
- **Structure**: 
  ```json
  {
    "packaging": [
      {
        "id": 1,
        "name": "Original Sleeve"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 6: Countries JSON File
- **File**: `data/countries.json`
- **Source**: Extract from `<country><displayname>` tags
- **Structure**: 
  ```json
  {
    "countries": [
      {
        "id": 1,
        "name": "USA"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

### ✅ Step 7: Stores JSON File
- **File**: `data/stores.json`
- **Source**: Extract from `<store><displayname>` tags
- **Structure**: 
  ```json
  {
    "stores": [
      {
        "id": 1,
        "name": "Record Store Name"
      }
    ]
  }
  ```
- **Status**: ✅ Complete

## Phase 2: Main Entity Extraction (After Phase 1)

### ⏳ Step 8: Albums JSON File
- **File**: `data/albums.json`
- **Source**: Extract from `<music>` tags with `<title>` elements
- **Structure**: Include references to artist_id, genre_ids, label_id, format_id, etc.
- **Status**: 🔲 Waiting for Phase 1 completion

### ⏳ Step 9: Tracks JSON File
- **File**: `data/tracks.json`
- **Source**: Extract track information from album sections
- **Status**: 🔲 Waiting for Step 8 completion

## Technical Implementation

### Data Folder Structure
```
data/
├── artists.json
├── genres.json
├── labels.json
├── formats.json
├── packaging.json
├── countries.json
├── stores.json
├── albums.json
└── tracks.json
```

### C# Implementation Components
- **XmlParser.cs**: Parse the MusicCollectorzExport.xml file
- **EntityExtractor.cs**: Extract unique entities and assign IDs
- **JsonGenerator.cs**: Generate JSON files with proper structure
- **UniqueIdGenerator.cs**: Generate consistent unique IDs

### ID Generation Strategy
- Artists: `1`, `2`, `3`, etc.
- Genres: `1`, `2`, `3`, etc.
- Labels: `1`, `2`, `3`, etc.
- Formats: `1`, `2`, `3`, etc.
- Packaging: `1`, `2`, `3`, etc.
- Countries: `1`, `2`, `3`, etc.
- Stores: `1`, `2`, `3`, etc.

## Current Task
**🎯 Ready to start with Step 1: Artists JSON File**

Once the artists.json file is completed, I will prompt you for the next step to proceed with the genres extraction.

---
*Created: August 13, 2025*
*Last Updated: August 13, 2025*
