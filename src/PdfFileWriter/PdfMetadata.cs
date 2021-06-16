﻿/////////////////////////////////////////////////////////////////////
//
//	PdfFileWriter
//	PDF File Write C# Class Library.
//
//	PdfMetadata
//
//	Uzi Granot
//	Version: 1.0
//	Date: April 1, 2013
//	Copyright (C) 2013-2019 Uzi Granot. All Rights Reserved
//
//	PdfFileWriter C# class library and TestPdfFileWriter test/demo
//  application are free software.
//	They is distributed under the Code Project Open License (CPOL).
//	The document PdfFileWriterReadmeAndLicense.pdf contained within
//	the distribution specify the license agreement and other
//	conditions and notes. You must read this document and agree
//	with the conditions specified in order to use this software.
//
//	For version history please refer to PdfDocument.cs
//
/////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace PdfFileWriter
	{
	/// <summary>
	/// PDF metadata class
	/// </summary>
	internal class PdfMetadata : PdfObject
		{
		/// <summary>
		/// PDF metadata constructor
		/// </summary>
		/// <param name="Document">PDF document</param>
		/// <param name="FileName">Metadata file name</param>
		public PdfMetadata
				(
				PdfDocument Document,
				string FileName
				) : base(Document, ObjectType.Stream, "/Metadata")
			{
			// test exitance
			if(!File.Exists(FileName)) throw new ApplicationException("Metadata file " + FileName + " does not exist");

			// get file length
			FileInfo FI = new FileInfo(FileName);
			if(FI.Length > int.MaxValue - 4095) throw new ApplicationException("Metadata file " + FileName + " too long");
			int FileLength = (int) FI.Length;

			// file data content byte array
			byte[] Metadata = new byte[FileLength];

			// load all the file's data
			FileStream DataStream = null;
			try
				{
				// open the file
				DataStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);

				// read all the file
				if(DataStream.Read(Metadata, 0, FileLength) != FileLength) throw new Exception();
				}

			// loading file failed
			catch(Exception)
				{
				throw new ApplicationException("Reading metadata file: " + FileName + " failed");
				}

			// close the file
			DataStream.Close();

			// create object
			CreateObject(Metadata);
			return;
			}

		/// <summary>
		/// PDF metadata constructor
		/// </summary>
		/// <param name="Document">PDF document</param>
		/// <param name="Metadata">Metadata binary array</param>
		public PdfMetadata
				(
				PdfDocument Document,
				byte[] Metadata
				) : base(Document, ObjectType.Stream, "/Metadata")
			{
			CreateObject(Metadata);
			return;
			}

		private void CreateObject
				(
				byte[] Metadata
				)
			{ 
			// test for first time
			if(Document.CatalogObject.Dictionary.Find("/Metadata") >= 0) throw new ApplicationException("Metadata is already defined");

			// add metadata object to catalog object
			Document.CatalogObject.Dictionary.AddIndirectReference("/Metadata", this);

			// add subtype
			Dictionary.Add("/Subtype", "/XML");

			// metadata to object value array
			ObjectValueArray = Metadata; 

			// no compression
			NoCompression = true;

			// no encryption
			PdfEncryption SaveEncryption = Document.Encryption;
			Document.Encryption = null;

			// write stream
			WriteToPdfFile();

			// restore encryption
			Document.Encryption = SaveEncryption;
			return;
			}
		}
	}
