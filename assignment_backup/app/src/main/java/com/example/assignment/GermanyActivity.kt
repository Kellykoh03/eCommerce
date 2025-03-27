package com.example.assignment

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageButton

class GermanyActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_germany)

        val imageButtonSalad = findViewById<ImageButton>(R.id.imageButtonSalad)
        val imageButtonPancake = findViewById<ImageButton>(R.id.imageButtonPancake)
        val imageButtonPudding = findViewById<ImageButton>(R.id.imageButtonPudding)
        val buttonSalad = findViewById<Button>(R.id.buttonSalad)
        val buttonPancake = findViewById<Button>(R.id.buttonPancake)
        val buttonPudding = findViewById<Button>(R.id.buttonPudding)

        imageButtonSalad.setOnClickListener {
            val intent = Intent(this, SaladActivity::class.java)
            startActivity(intent)
        }

        imageButtonPancake.setOnClickListener {
            val intent = Intent(this, PancakeActivity::class.java)
            startActivity(intent)
        }

        imageButtonPudding.setOnClickListener {
            val intent = Intent(this, PuddingActivity::class.java)
            startActivity(intent)
        }

        buttonSalad.setOnClickListener {
            val intent = Intent(this, SaladActivity::class.java)
            startActivity(intent)
        }

        buttonPancake.setOnClickListener {
            val intent = Intent(this, PancakeActivity::class.java)
            startActivity(intent)
        }

        buttonPudding.setOnClickListener {
            val intent = Intent(this, PuddingActivity::class.java)
            startActivity(intent)
        }
    }
}