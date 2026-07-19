# Ontology-Based Adaptive Learning Research

This repository presents a historical research portfolio on ontology-based adaptive assessment, semantic reasoning, and knowledge representation in educational systems.

The work originated from research conducted during my MSc in Information Systems and resulted in three peer-reviewed publications addressing:

- Domain ontology construction
- Semantic adaptive examination
- Knowledge-based learner assessment
- Comparison between ontology-based adaptation and Item Response Theory
- Arabic financial-accounting knowledge representation

## Research Background

Traditional computer-based examinations often select questions using fixed rules or statistical difficulty estimates. This research investigated whether semantic technologies could improve adaptive assessment by representing relationships among concepts, learning objectives, questions, and learner knowledge.

The proposed approach used ontologies and semantic reasoning to select questions dynamically according to:

- The learner’s demonstrated knowledge
- Relationships among domain concepts
- Question difficulty and prerequisite structure
- Previous responses
- The semantic coverage required by the assessment

The principal application domain was Arabic financial accounting.

## Research Objectives

The research addressed the following objectives:

1. Develop a domain-specific ontology for financial accounting.
2. Represent educational concepts, questions, learning relationships, and assessment rules semantically.
3. Design an ontology-based adaptive examination engine.
4. Use semantic reasoning to personalize question selection.
5. Compare ontology-based adaptation with Item Response Theory.
6. Assess the feasibility of semantic technologies in e-learning management systems.

## Main Research Components

### 1. Arabic Financial-Accounting Ontology

A domain ontology was designed to represent financial-accounting concepts and their semantic relationships.

The ontology supported:

- Hierarchical concept organization
- Prerequisite relationships
- Topic and subtopic representation
- Question-to-concept mapping
- Semantic relationships among accounting concepts
- Reasoning over learner knowledge and examination content

### 2. Ontology-Based Adaptive Examination Engine

The adaptive examination engine used semantic information to determine which question should be presented next.

The system incorporated:

- Learner-performance tracking
- Semantic concept coverage
- Rule-based question selection
- Difficulty adaptation
- Knowledge-gap identification
- Dynamic examination progression

### 3. Comparison with Item Response Theory

The research compared ontology-based adaptive assessment with Item Response Theory.

The comparison examined differences in:

- Adaptation logic
- Knowledge representation
- Question selection
- Interpretation of learner performance
- Domain-awareness
- Semantic explainability

The ontology-based approach emphasized explicit domain knowledge and relationships, while Item Response Theory relied primarily on statistical modeling of learner ability and item characteristics.

## Technologies

The research used technologies including:

- RDF
- OWL
- SPARQL
- Protégé
- Semantic reasoning
- C#
- ASP.NET
- SQL
- Ontology-driven application design

## Publications

### Arabic Ontology Model for Financial Accounting

**Eman Khater, A. Hegazy, and M. Sakre**

Published in the *International Conference on Soft Computing and Software Engineering, SCSE 2015*.

[View publication](https://www.sciencedirect.com/science/article/pii/S1877050915026599)

### Ontology-Based Adaptive Examination System in E-Learning Management Systems

**Eman Khater, A. Hegazy, and M. E. Shehab**

Published in the *IEEE Seventh International Conference on Intelligent Computing and Information Systems, ICICIS 2015*.

[View publication](https://ieeexplore.ieee.org/document/7397228)

### Comparing Ontology-Based and Item Response Theory in Computer Adaptive Test

**Eman Khater, A. Hegazy, and M. E. Shehab**

Published in the *IEEE Seventh International Conference on Intelligent Computing and Information Systems, ICICIS 2015*.

[View publication](https://ieeexplore.ieee.org/document/7397220)

## Relationship Among the Publications

The three publications represent related parts of one broader research program:

1. The financial-accounting ontology defined the domain knowledge.
2. The adaptive examination system used that ontology for semantic question selection.
3. The comparative study evaluated the ontology-based approach against Item Response Theory.

Together, they demonstrate how knowledge representation and semantic reasoning can support adaptive educational systems.

## Repository Scope

This repository is intended as a historical research portfolio rather than a complete software reproduction package.

It may include:

- Research summaries
- Architecture descriptions
- Original diagrams
- Ontology samples
- Selected code fragments owned by the author
- Citation metadata
- Links to official publications

Some original implementation materials may no longer be available or may depend on obsolete software environments.

Any reconstructed material will be clearly identified as reconstructed rather than presented as an original archived artifact.

## Planned Repository Structure

```text
.
├── architecture/
├── figures/
├── ontology/
├── references/
├── docs/
├── CITATION.cff
└── README.md
