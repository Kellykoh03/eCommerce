package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton

class MalaysiaActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_malaysia)

        val imageButtonRojak = findViewById<ImageButton>(R.id.imageButtonRojak)
        val imageButtonBegedil = findViewById<ImageButton>(R.id.imageButtonBegedil)
        val imageButtonBubur = findViewById<ImageButton>(R.id.imageButtonBubur)
        val buttonRojak = findViewById<Button>(R.id.buttonRojak)
        val buttonBegedil = findViewById<Button>(R.id.buttonBegedil)
        val buttonBubur = findViewById<Button>(R.id.buttonBubur)

        imageButtonRojak.setOnClickListener {
            val intent = Intent(this, FruitRojakActivity::class.java)
            startActivity(intent)
        }

        imageButtonBegedil.setOnClickListener {
            val intent = Intent(this, BegedilActivity::class.java)
            startActivity(intent)
        }

        imageButtonBubur.setOnClickListener {
            val intent = Intent(this, BuburActivity::class.java)
            startActivity(intent)
        }

        buttonRojak.setOnClickListener {
            val intent = Intent(this, FruitRojakActivity::class.java)
            startActivity(intent)
        }

        buttonBegedil.setOnClickListener {
            val intent = Intent(this, BegedilActivity::class.java)
            startActivity(intent)
        }

        buttonBubur.setOnClickListener {
            val intent = Intent(this, BuburActivity::class.java)
            startActivity(intent)
        }
    }
}